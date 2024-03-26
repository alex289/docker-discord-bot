using FluentValidation;

namespace DockerDiscordBot.Commands.RemoveContainer;

public sealed class RemoveContainerCommandValidation : CommandValidation<RemoveContainerCommand>
{
    public RemoveContainerCommandValidation()
    {
        AddRuleForMessageContent(1, 1);
        AddRuleForContainerId();
    }

    private void AddRuleForContainerId()
    {
        RuleFor(x => x.ContainerId)
            .NotEmpty()
            .WithMessage("Container id cannot be empty.");
    }
}