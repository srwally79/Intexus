import { RouterModule, Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: 'tareas',
        loadChildren: () => import('./tareas/tareas.module').then(m => m.TareasModule)
      },
      {
        path: '**',
        redirectTo: 'tareas'
      }
];