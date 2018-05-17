import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { TaskDashboardComponent } from './task.component.dashboard.js';
import { TaskListComponent } from './task.component.list.js';
import { TaskSubmitComponent } from './task.component.submit.js';

const routes: Routes = [
    { path: '', redirectTo: '/Dashboard', pathMatch: 'full' },
    { path: 'Dashboard', component: TaskDashboardComponent },
    { path: 'List/:type', component: TaskListComponent },
    { path: 'Submit/:id', component: TaskSubmitComponent },
    { path: 'Submit', component: TaskSubmitComponent }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class TaskRouting { }

