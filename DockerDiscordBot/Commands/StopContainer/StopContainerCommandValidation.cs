using FluentValidation;

namespace DockerDiscordBot.Commands.StopContainer;

public sealed class StopContainerCommandValidation : CommandValidation<StopContainerCommand>
{
    public StopContainerCommandValidation()
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