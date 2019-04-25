import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { BsDatepickerModule } from 'ngx-bootstrap';

import { SafePipe } from '../common/module.safepipe.js';
import { TaskDetailComponent } from './task.detail.component.js';
import { TaskService } from './task.service.js';

@NgModule({
    imports: [BrowserModule, HttpClientModule, FormsModule, BsDatepickerModule.forRoot()],
    declarations: [TaskDetailComponent, SafePipe],
    bootstrap: [TaskDetailComponent],
    providers: [TaskService]
})
export class AppModule { }
