<template>
  <section>
    <base-card>
      <h2 v-if="IsActUpdate">Update an employee</h2>
       <h2 v-else>Register as a employee now!</h2>
      <employee-form @save-data="saveData" :id="getEmpID"></employee-form>
    </base-card>
  </section>
</template>

<script>
import EmployeeForm from '../../components/employees/EmployeeForm.vue';

export default {
  props: ['id'],
  components: {
    EmployeeForm,
  },
  computed:{
    getEmpID() 
    {
      return this.id;
    },
    IsActUpdate() 
    {
      return this.id != null;
    }
  },
  methods: {
    saveData(data) {
      this.$store.dispatch('employees/registerEmployee', data);
      this.$router.replace('/employees');
    }
  }
};
</script>