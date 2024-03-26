using FluentValidation;

namespace DockerDiscordBot.Commands;

public abstract class CommandValidation<T> : AbstractValidator<T> where T : Command
{
    protected void AddRuleForMessageContent(
        int? parameterMinAmount = null,
        int? parameterMaxAmount = null)
    {
        if (parameterMinAmount is null && parameterMaxAmount is null)
        {
            return;
        }
        
        RuleFor(x => x.Message.Content)
            .NotEmpty()
            .WithMessage("Message content cannot be empty.");
        
        if (parameterMinAmount is not null && parameterMaxAmount is not null)
        {
            var message = parameterMinAmount == parameterMaxAmount
                ? $"Message content must have exactly {parameterMinAmount} parameter(s)."
                : $"Message content must have between {parameterMinAmount} and {parameterMaxAmount} parameter(s).";
            
            RuleFor(x => x.Message.Content.Split(" ", StringSplitOptions.None))
                .Must(x => x.Length >= parameterMinAmount + 1 && x.Length <= parameterMaxAmount + 1)
                .WithMessage(message);
        }
        else if (parameterMinAmount is not null)
        {
            RuleFor(x => x.Message.Content.Split(" ", StringSplitOptions.None))
                .Must(x => x.Length >= parameterMinAmount + 1)
                .WithMessage($"Message content must have at least {parameterMinAmount} parameter(s).");
        }
        else if (parameterMaxAmount is not null)
        {
            RuleFor(x => x.Message.Content.Split(" ", StringSplitOptions.None))
                .Must(x => x.Length <= parameterMaxAmount + 1)
                .WithMessage($"Message content must have at most {parameterMaxAmount} parameter(s).");
        }
    }
}