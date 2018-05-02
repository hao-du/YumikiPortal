import { Component } from '@angular/core';

import { Project } from './project.model.js'

@Component({
    selector: 'ontime',
    templateUrl: '/Apps/OnTime/Project/List',
})
export class ProjectListComponent {
    project: Project;

    constructor() {
        this.project = new Project('Real Project', 'This is real project.');
    }
}
