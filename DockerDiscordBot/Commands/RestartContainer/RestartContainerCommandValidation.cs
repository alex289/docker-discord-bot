using FluentValidation;

namespace DockerDiscordBot.Commands.RestartContainer;

public sealed class RestartContainerCommandValidation : CommandValidation<RestartContainerCommand>
{
    public RestartContainerCommandValidation()
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