#include "JGame_InteractorComponent.h"
#include "JGame_InteractableInterface.h"

// Sets default values for this component's properties
UJGame_InteractorComponent::UJGame_InteractorComponent()
{
	
}

AActor* UJGame_InteractorComponent::InteractionTrace(float Distance, ETraceTypeQuery Channel, UCameraComponent* Camera, bool Print)
{
	// If the Interaction Line Trace is Active...
	if (DoTrace)
	{
		// Trace from the Camera Location to a point [Distance] units away
		FVector Start = Camera->GetComponentLocation();
		FVector End = Camera->GetComponentLocation() + (Camera->GetForwardVector() * Distance);

		FHitResult HitResult;

		FCollisionQueryParams CollisionParams;
		CollisionParams.bTraceComplex = true;
		CollisionParams.AddIgnoredActor(this->GetOwner());

		// If something is hit...
		if (GetWorld()->LineTraceSingleByChannel(HitResult, Start, End,
			UEngineTypes::ConvertToCollisionChannel(Channel), CollisionParams))
		{
			HitActor = HitResult.GetActor();

			// Check if the Hit Actor is an Interactable Object
			if (HitActor->GetClass()->ImplementsInterface(UJGame_InteractableInterface::StaticClass()))
			{
				LookingAtInteractable = true;

				if (Print)
					UE_LOG(LogTemp, Display, TEXT("JGAME LOG: Hit Interactable Actor"));
				
				return HitActor;
			}
		}
		else
		{
			LookingAtInteractable = false;
			HitActor = nullptr;
			
			return HitActor;
		}
	}
	LookingAtInteractable = false;
	HitActor = nullptr;
	
	return HitActor;
}

void UJGame_InteractorComponent::Interact(InputEvent InputEvents, AActor* InteractionActor, InteractionType Tap,
	InteractionType Hold, float HoldTime, bool Print)
{
	FInteractionParameters InteractionParams;
	InteractionParams.InteractionActor = InteractionActor;
	InteractionParams.Print = Print;
	
	if (InputEvents == InputEvent::Pressed)
	{
		InteractionParams.Type = Hold;
		InteractionParams.SetBool = true;

		FTimerDelegate HoldDelegate;
		HoldDelegate.BindUFunction(this, FName("AttemptInteraction"), InteractionParams);

		GetWorld()->GetTimerManager().SetTimer(HoldInputTimerHandle, HoldDelegate, HoldTime, false);

		if (Print)
			UE_LOG(LogTemp, Display, TEXT("Interact Key Pressed"));
	}

	else if (InputEvents == InputEvent::Released)
	{
		if (!isTapBlocked)
		{
			InteractionParams.Type = Tap;
			InteractionParams.SetBool = false;
			
			AttemptInteraction(InteractionParams);

			GetWorld()->GetTimerManager().ClearTimer(HoldInputTimerHandle);
		}

		else if (isTapBlocked)
			isTapBlocked = false;

		if (Print)
			UE_LOG(LogTemp, Display, TEXT("Interact Key Released"));
	}
}

void UJGame_InteractorComponent::AttemptInteraction(FInteractionParameters InteractionParameters)
{
	if (InteractionParameters.InteractionActor)
	{
		if (LookingAtInteractable)
		{
			IJGame_InteractableInterface::Execute_ReceiveInteraction(InteractionParameters.InteractionActor, GetOwner(),
				InteractionParameters.Type);

			if (InteractionParameters.Print)
				UE_LOG(LogTemp, Display, TEXT("JGAME LOG: Interaction Attempted"));
		}

		if (InteractionParameters.SetBool)
		{
			isTapBlocked = true;
		}
	}

	if (InteractionParameters.Print)
	{
		switch (InteractionParameters.Type)
		{
		case InteractionType::None:
			UE_LOG(LogTemp, Display, TEXT("JGAME LOG: Interaction Type: None"))
			break;

		case InteractionType::Use:
			UE_LOG(LogTemp, Display, TEXT("JGAME LOG: Interaction Type: Use"))
			break;

		case InteractionType::Pickup:
			UE_LOG(LogTemp, Display, TEXT("JGAME LOG: Interaction Type: Pickup"))
			break;
		}
	}
}
