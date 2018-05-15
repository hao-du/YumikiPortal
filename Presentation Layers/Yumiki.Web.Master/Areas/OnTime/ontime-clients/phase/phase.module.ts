import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { PhaseListComponent } from './phase.component.list.js';
import { PhaseSubmitComponent } from './phase.component.submit.js';

import { PhaseService } from './phase.service.js';

@NgModule({
    imports: [BrowserModule, FormsModule, HttpClientModule],
    declarations: [PhaseListComponent, PhaseSubmitComponent],
    bootstrap: [PhaseListComponent],
    providers: [PhaseService]
})
export class AppModule { }
