import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { ProjectListComponent } from './project.component.list.js';

@NgModule({
  imports:      [ BrowserModule ],
  declarations: [ProjectListComponent ],
  bootstrap: [ProjectListComponent ]
})
export class AppModule { }
