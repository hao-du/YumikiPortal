import { Component, OnInit } from '@angular/core';

import { Guid } from '../common/guid.js'
import { Project } from '../models/project.model.js'

import { ProjectService } from './project.service.js';

declare var yumiki: any;

@Component({
    selector: 'ontime',
    templateUrl: '/Apps/OnTime/Project/List',
})
export class ProjectListComponent implements OnInit {
    projects: Project[];
    selectedProject: Project;

    constructor(private service: ProjectService) {
    }

    ngOnInit() {
        yumiki.message.displayLoadingDialog(true);

        this.service.getProjects().subscribe(projects => {
            this.projects = projects;
            console.log(projects);

            yumiki.message.displayLoadingDialog(false);
        });
    }

    onSelect(project: Project): void {
        if (!project) {
            project = new Project();
            project.ID = Guid.empty;

            this.selectedProject = project;
            console.log("Add with ID: " + project.ID);
        }
        else {
            if (project && project.ID != Guid.empty) {
                yumiki.message.displayLoadingDialog(true);

                this.service.getProject(project.ID).subscribe(result => {
                    console.log(project);

                    project.ProjectName = result.ProjectName;
                    project.AssignedNumber = result.AssignedNumber;
                    project.Prefix = result.Prefix;
                    project.Descriptions = result.Descriptions;
                    project.IsActive = result.IsActive;
                    project.LastUpdateDateUI = result.LastUpdateDateUI;

                    console.log(project);

                    this.selectedProject = project;
                    console.log("Edit with ID: " + project.ID);

                    yumiki.message.displayLoadingDialog(false);
                });
            }
        }

        console.log("Selected ID: " + project.ID);
    }
}
