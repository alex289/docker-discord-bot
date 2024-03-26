using FluentValidation;

namespace DockerDiscordBot.Commands.ShowContainer;

public sealed class ShowContainerCommandValidation : CommandValidation<ShowContainerCommand>
{
    public ShowContainerCommandValidation()
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