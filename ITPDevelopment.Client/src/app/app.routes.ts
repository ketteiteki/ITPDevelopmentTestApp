import {RouterModule, Routes} from '@angular/router';
import {LayoutComponent} from "./pages/layout/layout.component";
import {TaskTableComponent} from "./components/task-table/task-table.component";
import {ProjectTableComponent} from "./components/project-table/project-table.component";
import {NgModule} from "@angular/core";

export const routes: Routes = [
  {
    path: '', component: LayoutComponent,
    children: [
      { path: 'tasks', component: TaskTableComponent },
      { path: 'projects', component: ProjectTableComponent },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }