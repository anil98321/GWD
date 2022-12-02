import { createStore } from 'vuex';

import employeesModule from './modules/employees/index.js';

const store = createStore({
  modules: {
    employees: employeesModule
  },
  state() {
    return {
      userId: 'c3'
    };
  },
  getters: {
    userId(state) {
      return state.userId;
    }
  }
});

export default store;