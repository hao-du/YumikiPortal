import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
import { enableProdMode } from '@angular/core';

import { AppModule } from './phase.module.js';

enableProdMode();
platformBrowserDynamic().bootstrapModule(AppModule);
