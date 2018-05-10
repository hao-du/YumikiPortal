import { Component, OnInit, Input } from '@angular/core';

import { Project } from '../models/project.model.js';

@Component({
    selector: 'project-submit',
    templateUrl: '/Apps/OnTime/Project/Submit',
})
export class ProjectSubmitComponent implements OnInit {
    @Input() project?: Project;

    constructor() {
    }

    ngOnInit() {
    }

    onClose() {
        this.project = undefined;
    }
}
