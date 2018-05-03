import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';

import { ProjectListComponent } from './project.component.list.js';
import { ProjectSubmitComponent } from './project.component.submit.js';

@NgModule({
  imports:      [ BrowserModule, FormsModule ],
  declarations: [ ProjectListComponent, ProjectSubmitComponent ],
  bootstrap: [ProjectListComponent ]
})
export class AppModule { }
