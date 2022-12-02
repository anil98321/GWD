<template>
  <li>
    <h3>{{ fullName }}</h3>
    <h4>{{ contact }}</h4>
    <h4>${{ monthlySalary }}</h4>    
    <div class="actions">
       <base-button link :to="employeeDetailsLink">View</base-button> 
       <base-button link :to="editEmployeeLink">Edit</base-button>
       <base-button @click="deleteEmployee(id)">Delete</base-button>
    </div>
  </li>
</template>

<script>
export default {
  props: ['id', 'firstName', 'lastName', 'email', 'monthlySalary'],
  computed: {
    fullName() {
      return this.firstName + ' ' + this.lastName;
    },
    contact() {
      return this.email; 
    },
    employeeDetailsLink() {
      return this.$route.path + '/' + this.id; 
    },
    editEmployeeLink() {
      return 'register/' + this.id; 
    },
  },
  methods: {
    deleteEmployee(id) 
    {
      const data = {id: id}
      this.$store.dispatch('employees/deleteEmployee', data);
      this.$router.replace('/employees');
    }
  }
};
</script>

<style scoped>
li {
  margin: 1rem 0;
  border: 1px solid #424242;
  border-radius: 12px;
  padding: 1rem;
}

h3 {
  font-size: 1.5rem;
}

h3,
h4 {
  margin: 0.5rem 0;
}

div {
  margin: 0.5rem 0;
}

.actions {
  display: flex;
  justify-content: flex-end;
}
</style>