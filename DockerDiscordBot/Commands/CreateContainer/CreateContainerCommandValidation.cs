using FluentValidation;

namespace DockerDiscordBot.Commands.CreateContainer;

public sealed class CreateContainerCommandValidation : CommandValidation<CreateContainerCommand>
{
    public CreateContainerCommandValidation()
    {
        AddRuleForMessageContent(2, 3);
        AddRuleForImageId();
        AddRuleForContainerName();
    }
    
    private void AddRuleForImageId()
    {
        RuleFor(x => x.ImageId)
            .NotEmpty()
            .WithMessage("Image id cannot be empty.");
    }

    private void AddRuleForContainerName()
    {
        RuleFor(x => x.ContainerName)
            .NotEmpty()
            .WithMessage("Container name cannot be empty.");
    }
}