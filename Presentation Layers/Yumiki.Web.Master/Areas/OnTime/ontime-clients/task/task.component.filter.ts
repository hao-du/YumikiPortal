import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
    selector: 'task-filter',
    templateUrl: '/Apps/OnTime/Task/Filter',
})
export class TaskFilterComponent implements OnInit {
    phaseID?: string;
    projectID?: string;
    @Output() messageEvent = new EventEmitter<string>();

    isControlInitialized: boolean = false;

    constructor() {
    }

    ngOnInit() {
        this.phaseID = this.projectID = '';
    }

    onFilterChange() {
        this.messageEvent.emit(this.phaseID + '|' + this.projectID);
    }
}
