import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { ProjectListComponent } from './project.component.list.js';
import { ProjectSubmitComponent } from './project.component.submit.js';

import { ProjectService } from './project.service.js';

@NgModule({
    imports: [BrowserModule, FormsModule, HttpClientModule],
    declarations: [ProjectListComponent, ProjectSubmitComponent],
    bootstrap: [ProjectListComponent],
    providers: [ProjectService]
})
export class AppModule { }
