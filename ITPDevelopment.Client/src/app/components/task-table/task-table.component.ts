import {Component, OnDestroy, OnInit} from '@angular/core';
import {MenuItem, PrimeIcons} from "primeng/api";
import {TaskStateService} from "../../services/state/task-state.service";
import {ITaskEntity} from "../../types/interfaces/ITaskEntity";
import {DateService} from "../../services/common/date.service";
import {LayoutStateService} from "../../services/state/layout-state.service";
import {debounceTime, fromEvent, Subscription} from "rxjs";

@Component({
  selector: 'app-task-table',
  templateUrl: './task-table.component.html',
  styleUrl: './task-table.component.scss'
})
export class TaskTableComponent implements OnInit, OnDestroy {
  // observable
  public tasks$ = this._taskState.tasks$;
  public documentScrollSub$: Subscription | undefined;

  // state
  public selectedTaskForContextMenu: ITaskEntity | undefined;

  theadMenuItems: MenuItem[] = [
    { label: 'Create Task', icon: PrimeIcons.PLUS, command: () => this.menuItemCreateTask() },
  ]
  tbodyTdMenuItems: MenuItem[] = [
    { label: 'Start', icon: PrimeIcons.CARET_RIGHT, command: () => this.menuItemStartTask() },
    { label: 'End', icon: PrimeIcons.CHECK, command: () => this.menuItemEndTask() },
  ]

  constructor(
    protected _taskState:  TaskStateService,
    protected _layoutState: LayoutStateService,
    protected _dateService: DateService
  ) {
  }

  async ngOnInit() {
    this.documentScrollSub$ = fromEvent(document, "scroll")
      .pipe(
        debounceTime(400))
      .subscribe(async e => {
        const event = e as Event;
        const target = event.target as Document;
        if (target.body.scrollHeight - window.innerHeight <= window.scrollY + 15) {
          const offset = this._taskState.tasks$.getValue().length;
          await this._taskState.getTaskListAsync(offset, 30);
        }
      });
  }

  ngOnDestroy() {
    this.documentScrollSub$?.unsubscribe();
  }

  // events
  public onClickOpenTaskDescriptionHandler(task: ITaskEntity) {
    this._layoutState.openTaskDescriptionWindow(task);
  }

  // MenuItem methods
  public async menuItemCreateTask() {
    this._layoutState.openCreateTaskWindow();
  }

  public async menuItemStartTask() {
    if (!this.selectedTaskForContextMenu) throw new Error("Selected task not found");

    await this._taskState.startTaskAsync(this.selectedTaskForContextMenu.id);
  }

  public async menuItemEndTask() {
    if (!this.selectedTaskForContextMenu) throw new Error("Selected task not found");

    await this._taskState.endTaskAsync(this.selectedTaskForContextMenu.id);
  }

  // common
  public setSelectedTaskForContextMenu(task: ITaskEntity) {
    this.selectedTaskForContextMenu = task;
  }

  protected readonly console = console;
}
