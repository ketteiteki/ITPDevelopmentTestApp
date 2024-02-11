import { Injectable } from '@angular/core';
import {ITaskEntity} from "../../types/interfaces/ITaskEntity";

@Injectable({
  providedIn: 'root'
})
export class LayoutStateService {
  public isCreateProjectWindowOpen = false;

  public isCreateTaskWindowOpen = false;

  public isTaskDescriptionWindowOpen = false;
  public TaskForViewDescription: ITaskEntity | undefined;

  public openCreateProjectWindow() {
    this.isCreateProjectWindowOpen = true;
    this.isCreateTaskWindowOpen = false;
    this.isTaskDescriptionWindowOpen = false;
  }

  public openCreateTaskWindow() {
    this.isCreateProjectWindowOpen = false;
    this.isCreateTaskWindowOpen = true;
    this.isTaskDescriptionWindowOpen = false;
  }

  public openTaskDescriptionWindow(task: ITaskEntity) {
    this.isCreateProjectWindowOpen = false;
    this.isCreateTaskWindowOpen = false;
    this.isTaskDescriptionWindowOpen = true;
    this.TaskForViewDescription = task;
  }

  public closeAll() {
    this.isCreateProjectWindowOpen = false;
    this.isCreateTaskWindowOpen = false;
    this.isTaskDescriptionWindowOpen = false;
  }
}
