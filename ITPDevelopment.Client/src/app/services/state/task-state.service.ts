import { Injectable } from '@angular/core';
import {BehaviorSubject, firstValueFrom} from "rxjs";
import {ITaskEntity} from "../../types/interfaces/ITaskEntity";
import {TaskApiService} from "../api/task-api.service";
import {CreateTaskRequest} from "../../types/requests/CreateTaskRequest";
import {UpdateTaskDescriptionRequest} from "../../types/requests/UpdateTaskDescriptionRequest";

@Injectable({
  providedIn: 'root'
})
export class TaskStateService {
  public tasks$: BehaviorSubject<ITaskEntity[]> = new BehaviorSubject<ITaskEntity[]>([]);

  constructor(
    private _taskApiService: TaskApiService
  ) { }

  public async getTaskListAsync(offset: number, limit: number) {
    const getTaskList$ = this._taskApiService.getTaskList(offset, limit);
    const getTaskListResult = await firstValueFrom(getTaskList$);

    const tasks = this.tasks$.getValue();
    tasks.push(...getTaskListResult.response);

    this.tasks$.next(tasks);

    return getTaskListResult;
  }

  public async createTaskAsync(request: CreateTaskRequest) {
    const postCreateTask$ = this._taskApiService.postCreateTask(request);
    const postCreateTaskResult = await firstValueFrom(postCreateTask$);

    const tasks = this.tasks$.getValue();
    tasks.push(postCreateTaskResult.response);

    this.tasks$.next(tasks);

    return postCreateTaskResult;
  }

  public async startTaskAsync(taskId: string) {
    const postStartTask$ = this._taskApiService.postStartTask(taskId);
    const postStartTaskResult = await firstValueFrom(postStartTask$);

    const tasks = this.tasks$.getValue();
    const startedTask = tasks.find(x => x.id === postStartTaskResult.response.id);

    if (!startedTask) throw new Error("Started task not found");

    startedTask.startDate = postStartTaskResult.response.startDate;

    this.tasks$.next(tasks);

    return postStartTaskResult;
  }

  public async endTaskAsync(taskId: string) {
    const postEndTask$ = this._taskApiService.postEndTask(taskId);
    const postEndTaskResult = await firstValueFrom(postEndTask$);

    const tasks = this.tasks$.getValue();
    const endedTask = tasks.find(x => x.id === postEndTaskResult.response.id);

    if (!endedTask) throw new Error("Ended task not found");

    endedTask.endDate = postEndTaskResult.response.endDate;

    this.tasks$.next(tasks);

    return postEndTaskResult;
  }

  public async updateTaskAsync(request: UpdateTaskDescriptionRequest) {
    const putUpdateTask$ = this._taskApiService.putUpdateTask(request);
    const putUpdateTaskResult = await firstValueFrom(putUpdateTask$);

    const tasks = this.tasks$.getValue();
    const updatedTask = tasks.find(x => x.id === putUpdateTaskResult.response.id);

    if (!updatedTask) throw new Error("Updated task not found");

    updatedTask.description = putUpdateTaskResult.response.description;

    this.tasks$.next(tasks);

    return putUpdateTaskResult;
  }
}
