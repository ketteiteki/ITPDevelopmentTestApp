
export class CreateTaskRequest {
  taskName: string;
  description: string;
  projectId: string;

  public constructor(taskName: string, description: string, projectId: string) {
    this.taskName = taskName;
    this.description = description;
    this.projectId = projectId;
  }
}