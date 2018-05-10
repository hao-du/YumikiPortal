import { Component, OnInit } from '@angular/core';

import { Project } from '../models/project.model.js'

import { ProjectService } from './project.service.js';

@Component({
    selector: 'ontime',
    templateUrl: '/Apps/OnTime/Project/List',
})
export class ProjectListComponent implements OnInit {
    projects: Project[];
    selectedProject : Project;

    constructor(private service: ProjectService) {
    }

    ngOnInit() {
        this.service.getProjects().subscribe(projects => {
            this.projects = projects;
            console.log(projects);
        });
    }

    onSelect(project : Project): void {
        this.selectedProject = project;
    }
}
