import { Component } from '@angular/core';

import { Project } from './models/project.js'

@Component({
    selector: 'my-app',
    templateUrl: '/areas/ontime/ontime-clients/views/project.welcome.html',
})
export class AppComponent {
    project: Project;

    constructor() {
        this.project = new Project('Test Project', 'This is test project.');
    }
}
