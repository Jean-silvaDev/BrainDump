using BrainDump.Application.UseCases.Auth;
using BrainDump.Application.UseCases.Review.ConfirmParsedTasks;
using BrainDump.Application.UseCases.Review.DiscardParsedTaskItem;
using BrainDump.Application.UseCases.Review.GetPendingReviewItems;
using BrainDump.Application.UseCases.Review.UpdateParsedTaskItem;
using BrainDump.Application.UseCases.Tasks.DeleteTask;
using BrainDump.Application.UseCases.Tasks.EditTask;
using BrainDump.Application.UseCases.Tasks.GetTasks;
using BrainDump.Application.UseCases.Tasks.ToggleTaskCompletion;
using BrainDump.Application.UseCases.VoiceEntries.ProcessVoiceEntryTranscription;
using BrainDump.Application.UseCases.VoiceEntries.RecordVoiceEntry;
using Microsoft.Extensions.DependencyInjection;

namespace BrainDump.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<RegisterUserUseCase>();
        services.AddScoped<LoginUseCase>();
        services.AddScoped<RefreshTokenUseCase>();
        services.AddScoped<RecordVoiceEntryUseCase>();
        services.AddScoped<ProcessVoiceEntryTranscriptionUseCase>();
        services.AddScoped<GetPendingReviewItemsUseCase>();
        services.AddScoped<UpdateParsedTaskItemUseCase>();
        services.AddScoped<ConfirmParsedTasksUseCase>();
        services.AddScoped<DiscardParsedTaskItemUseCase>();
        services.AddScoped<GetTasksUseCase>();
        services.AddScoped<ToggleTaskCompletionUseCase>();
        services.AddScoped<EditTaskUseCase>();
        services.AddScoped<DeleteTaskUseCase>();

        return services;
    }
}




