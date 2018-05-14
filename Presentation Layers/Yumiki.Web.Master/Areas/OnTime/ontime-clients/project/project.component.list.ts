import { Component, OnInit } from '@angular/core';

import { Guid } from '../common/guid.js'
import { Constants } from '../common/constants.js'
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
        this.getProjects();
    }

    onSelect(project: Project) {
        if (!project) {
            project = new Project();
            project.ID = Guid.empty;
            project.IsActive = true;

            this.selectedProject = project;
        }
        else {
            if (project && project.ID != Guid.empty) {
                yumiki.message.displayLoadingDialog(true);

                this.service.getProject(project.ID).subscribe(
                    result => {
                        this.selectedProject = project = result;

                        yumiki.message.displayLoadingDialog(false);
                    },
                    err => {
                        yumiki.message.displayLoadingDialog(false);
                        yumiki.message.clientMessage(err, '', Constants.ErrorType);
                    });
            }
        }
    }

    onRefreshData(message: string) {
        if (message == 'ok') {
            this.getProjects();
        }
    }

    getProjects() {
        yumiki.message.displayLoadingDialog(true);

        this.service.getProjects().subscribe(
            projects => {
                this.projects = projects;

                yumiki.message.displayLoadingDialog(false);
            },
            err => {
                yumiki.message.displayLoadingDialog(false);
                yumiki.message.clientMessage(err, '', Constants.ErrorType);
            });
    }
}
