import { createRouter, createWebHistory } from 'vue-router';

import EmployeeDetail from './pages/employees/EmployeeDetail.vue';
import EmployeesList from './pages/employees/EmployeesList.vue';
import EmployeeRegistation from './pages/employees/EmployeeRegistration.vue';
import NotFound from './pages/NotFound.vue';

const router = createRouter({
  history: createWebHistory(),
  routes: [
    { path: '/', redirect: '/employees' },
    { path: '/employees', component: EmployeesList },
    {
      path: '/employees/:id',
      component: EmployeeDetail,
      props: true
    },
    { path: '/register', component: EmployeeRegistation },
    { 
      path: '/register/:id', 
      component: EmployeeRegistation,
      props:true
    },
    { path: '/:notFound(.*)', component: NotFound }
  ]
});

export default router;
