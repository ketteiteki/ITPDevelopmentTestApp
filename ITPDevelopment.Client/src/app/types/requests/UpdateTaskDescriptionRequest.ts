

export class UpdateTaskDescriptionRequest {
  taskId: string;
  description: string;

  public constructor(taskId: string, description: string) {
    this.taskId = taskId;
    this.description = description;
  }
}