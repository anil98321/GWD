export default {
  registerEmployee(state, payload) {
    state.employees.push(payload);   
  },
  updateEmployee(state, payload) {
    const empIx = state.employees.findIndex(val => Number.parseInt(val.id) === payload.id);
    if(empIx > -1) {
      state.employees[empIx].firstName = payload.firstName;
      state.employees[empIx].lastName = payload.lastName;
      state.employees[empIx].monthlySalary = payload.monthlySalary;
      state.employees[empIx].email = payload.email;
    }     
  },
  deleteEmployee(state, payload) {
    const empIx = state.employees.findIndex(val => val.id === payload.id);
    if(empIx > -1) {
      state.employees.splice(empIx, 1);
    } 
  },
  setEmployees(state, payload) {   
    state.employees = payload;
  },
  setFetchTimestamp(state) {
    state.lastFetch = new Date().getTime();
  }
};