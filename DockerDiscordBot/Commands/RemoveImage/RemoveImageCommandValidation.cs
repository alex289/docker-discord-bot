using FluentValidation;

namespace DockerDiscordBot.Commands.RemoveImage;

public sealed class RemoveImageCommandValidation : CommandValidation<RemoveImageCommand>
{
    public RemoveImageCommandValidation()
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