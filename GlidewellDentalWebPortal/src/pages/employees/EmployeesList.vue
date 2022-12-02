<template>
  <div>
    <base-dialog :show="!!error" title="An error occurred!" @close="handleError">
      <p>{{ error }}</p>
    </base-dialog>   
    <section>
      <base-card>
        <div class="controls">
          <base-button mode="outline" @click="loadEmployees(true)">Refresh</base-button>
          <base-button v-if="!isLoading" link to="/register">Register as Employee</base-button>
        </div>
        <div v-if="isLoading">
          <base-spinner></base-spinner>
        </div>
        <ul v-else-if="hasEmployees">
          <employee-item
            v-for="employee in filteredEmployees"
            :key="employee.id"
            :id="employee.id"
            :first-name="employee.firstName"
            :last-name="employee.lastName"
            :email="employee.email"
            :monthlySalary="employee.monthlySalary"
          ></employee-item>
        </ul>
        <h3 v-else>No employees found.</h3>
      </base-card>
    </section>
  </div>
</template>

<script>
import EmployeeItem from '../../components/employees/EmployeeItem.vue';

export default {
  components: {
    EmployeeItem
  },
  data() {
    return {
      isLoading: false,
      error: null
    };
  },
  computed: {
    filteredEmployees() {
      return this.$store.getters['employees/employees'];
    },
    hasEmployees() {
      return !this.isLoading && this.$store.getters['employees/hasEmployees'];
    },
  },
  created() {
    this.loadEmployees();
  },
  methods: {
    async loadEmployees(refresh = false) {
      this.isLoading = true;
      try {
        await this.$store.dispatch('employees/loadEmployees', {
          forceRefresh: refresh,
        });
      } catch (error) {
        this.error = error.message || 'Something went wrong!';
      }
      this.isLoading = false;
    },
    handleError() {
      this.error = null;
    },
  },
};
</script>

<style scoped>
ul {
  list-style: none;
  margin: 0;
  padding: 0;
}

.controls {
  display: flex;
  justify-content: space-between;
}
</style>