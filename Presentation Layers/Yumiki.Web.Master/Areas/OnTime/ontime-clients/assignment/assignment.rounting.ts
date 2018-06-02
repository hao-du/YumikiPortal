import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { UserComponent } from './assignment.component.user.js';
import { AssignmentComponent } from './assignment.component.assignment.js';

const routes: Routes = [
    { path: '', redirectTo: '/User', pathMatch: 'full' },
    { path: 'User', component: UserComponent },
    { path: 'Assignment/:id', component: AssignmentComponent }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AssignmentRouting { }

