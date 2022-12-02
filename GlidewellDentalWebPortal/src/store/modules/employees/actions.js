import config from "../../../config";
export default {
  async registerEmployee(context, data) {    
    let employeeData = {
      firstName: data.firstName,
      lastName: data.lastName,
      email: data.email,
      monthlySalary: data.monthlySalary
    };

    const response = await fetch(
      data.id != null ? `${config.SERVICE_HOST_NAME}/api/employee/${data.id}` : `${config.SERVICE_HOST_NAME}/api/employee`,
      {
        method:  data.id != null ? 'PUT': 'POST',
        headers: 
        {
          'Content-Type': 'application/json;charset=UTF-8'
        },
        body: JSON.stringify( data.id != null 
          ? {
          ...employeeData,
          id: data.id
          }
          : employeeData)
      }
    );
    const responseData = await response.json();  

    let id = 0;
    if (response.ok) {      
      id = responseData.id;
    }
    else 
    {
      const error = new Error(responseData.message || 'Failed to fetch!');
      throw error;
    }

    if(data.id != null) {
      context.commit('updateEmployee', {
        ...employeeData,
        id: id
      });
    }
    else {
      context.commit('registerEmployee', {
        ...employeeData,
        id: id
      });
    }
    
  },
  async deleteEmployee(context, data) {    
    const response = await fetch(
      `${config.SERVICE_HOST_NAME}/api/employee/${data.id}`,
      {
        method: 'DELETE',
        headers: 
        {
          'Content-Type': 'application/json;charset=UTF-8'
        }
      }
    );
    
    if (!response.ok) {
      // error..
    }

    context.commit('deleteEmployee', {     
      id: data.id
    });
  },
  async loadEmployees(context, payload) {
    if (!payload.forceRefresh && !context.getters.shouldUpdate) {
      return;
    }

    const response = await fetch(
      `${config.SERVICE_HOST_NAME}/api/employee`
    );
    const responseData = await response.json();    

    if (!response.ok) {
      const error = new Error(responseData.message || 'Failed to fetch!');
      throw error;
    }

    const employees = [];

    for (const key in responseData) {
      const employee = {
        id: responseData[key].id,
        firstName: responseData[key].firstName,
        lastName: responseData[key].lastName,
        email: responseData[key].email,
        monthlySalary: responseData[key].monthlySalary
      };
      employees.push(employee);
    }
//console.log(employees);
    context.commit('setEmployees', employees);
    context.commit('setFetchTimestamp');
  }
};
