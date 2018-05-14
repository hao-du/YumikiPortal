import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { ListComponent } from './assignment.component.list.js';
import { UserComponent } from './assignment.component.user.js';
import { AssignmentComponent } from './assignment.component.assignment.js';

import { AssignmentService } from './assignment.service.js';

import { AssignmentRouting } from './assignment.rounting.js'

@NgModule({
    imports: [BrowserModule, FormsModule, HttpClientModule, AssignmentRouting],
    declarations: [ListComponent, UserComponent, AssignmentComponent],
    bootstrap: [ListComponent],
    providers: [AssignmentService]
})
export class AppModule { }
