export class Project {
    //fields
    projectName: string;
    projectDescription: string;
    isActive: boolean;

    //Constructor
    constructor(projectName: string, projectDescription: string) {
        this.projectName = projectName;
        this.projectDescription = projectDescription;
    }

    setStatus(isActive: boolean): void  {
        this.isActive = isActive;
    }
}
