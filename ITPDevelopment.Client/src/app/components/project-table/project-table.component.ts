import { Component } from '@angular/core';
import {MenuItem, PrimeIcons} from "primeng/api";
import {ProjectStateService} from "../../services/state/project-state.service";
import {DateService} from "../../services/common/date.service";
import {LayoutStateService} from "../../services/state/layout-state.service";

@Component({
  selector: 'app-project-table',
  templateUrl: './project-table.component.html',
  styleUrl: './project-table.component.scss'
})
export class ProjectTableComponent {
  public projects$ = this._projectState.projects$;

  theadMenuItems: MenuItem[] = [
    { label: 'Create Project', icon: PrimeIcons.PLUS, command: () => this.menuItemCreateProject() },
  ]

  constructor(
    protected _projectState: ProjectStateService,
    protected _layoutState: LayoutStateService,
    protected _dateService: DateService
  ) {
  }

  // MenuItem methods
  public async menuItemCreateProject() {
    this._layoutState.openCreateProjectWindow();
  }
}
