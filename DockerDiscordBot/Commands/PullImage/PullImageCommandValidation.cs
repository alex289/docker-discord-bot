using FluentValidation;

namespace DockerDiscordBot.Commands.PullImage;

public sealed class PullImageCommandValidation : CommandValidation<PullImageCommand>
{
    public PullImageCommandValidation()
    {
        AddRuleForMessageContent(1, 1);
        AddRuleForImageId();
    }

    private void AddRuleForImageId()
    {
        RuleFor(x => x.ImageId)
            .NotEmpty()
            .WithMessage("Image id cannot be empty.");
    }
}