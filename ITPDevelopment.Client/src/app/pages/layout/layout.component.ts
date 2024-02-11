import {Component, OnInit} from '@angular/core';
import {LayoutStateService} from "../../services/state/layout-state.service";
import {ProjectStateService} from "../../services/state/project-state.service";
import {TaskStateService} from "../../services/state/task-state.service";
import {CreateProjectRequest} from "../../types/requests/CreateProjectRequest";
import {CreateTaskRequest} from "../../types/requests/CreateTaskRequest";
import {UpdateTaskDescriptionRequest} from "../../types/requests/UpdateTaskDescriptionRequest";

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrl: './layout.component.scss'
})
export class LayoutComponent implements OnInit {
  // observable
  public projects$ = this.projectState.projects$;

  // create project state
  public projectName: string = "";

  // create task state
  public taskName: string = "";
  public selectedProjectId: string | undefined;
  public taskDescription: string = "";

  constructor(
    public layoutState: LayoutStateService,
    protected projectState: ProjectStateService,
    protected taskState: TaskStateService
  ) {
  }

  async ngOnInit() {
    await this.taskState.getTaskListAsync(0, 30);
    const projects = await this.projectState.getProjectListAsync();
    const firstProjectItem = projects.response[0];

    if (!firstProjectItem) return;

    this.selectedProjectId = firstProjectItem.id;
  }

  // events
  public async onClickCreateProjectHandler() {
    const createProjectRequest = new CreateProjectRequest(this.projectName);
    await this.projectState.createProjectAsync(createProjectRequest);

    this.projectName = "";
    this.layoutState.closeAll();
  }

  public async onClickCreateTaskHandler() {
    if (!this.selectedProjectId) throw new Error("Project id not selected");

    const createTaskRequest = new CreateTaskRequest(this.taskName, this.taskDescription, this.selectedProjectId);
    await this.taskState.createTaskAsync(createTaskRequest);

    this.taskName = "";
    this.taskDescription = "";
    this.layoutState.closeAll();
  }

  public async onClickUpdateTaskDescriptionHandler(description: string) {
    const taskId = this.layoutState.TaskForViewDescription?.id;

    if (!taskId) throw new Error("Task id not found");

    const updateTaskDescriptionRequest = new UpdateTaskDescriptionRequest(taskId, description);
    await this.taskState.updateTaskAsync(updateTaskDescriptionRequest);

    this.layoutState.closeAll();
  }
}
