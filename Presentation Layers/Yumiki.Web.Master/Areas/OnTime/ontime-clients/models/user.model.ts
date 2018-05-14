import { OnTimeBase } from './ontimebase.model.js';
import { Project } from './project.model.js';

export class User extends OnTimeBase {
    UserName: string;
    FullName: number;

    ProjectAssignments: Project;
}
