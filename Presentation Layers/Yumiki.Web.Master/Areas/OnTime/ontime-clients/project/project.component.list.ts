import { Component, OnInit } from '@angular/core';

import { Project } from './project.model.js'
import { Projects } from './project.models.js'

@Component({
    selector: 'ontime',
    templateUrl: '/Apps/OnTime/Project/List',
})
export class ProjectListComponent implements OnInit {
    projects = Projects;
    selectedProject : Project;

    constructor() {
    }

    ngOnInit() {
    }

    onSelect(project : Project): void {
        this.selectedProject = project;
    }
}
