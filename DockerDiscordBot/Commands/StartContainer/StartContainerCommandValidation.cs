using FluentValidation;

namespace DockerDiscordBot.Commands.StartContainer;

public sealed class StartContainerCommandValidation : CommandValidation<StartContainerCommand>
{
    public StartContainerCommandValidation()
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