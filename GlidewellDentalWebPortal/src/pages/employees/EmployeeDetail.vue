<template>
  <div>
    <section>
      <base-card>
        <h2>{{ fullName }}</h2>
        <h3>{{ email }}</h3>
        <h3>${{ salary }}/month</h3>
      </base-card>
    </section>
  </div>
</template>

<script>
export default {
  props: ['id'],
  data() {
    return {
      selectedEmployee: null,
    };
  },
  computed: {
    fullName() {
      return this.selectedEmployee.firstName + ' ' + this.selectedEmployee.lastName;
    },
    salary() {
      return this.selectedEmployee.monthlySalary;
    },
    email() {
      return this.selectedEmployee.email;
    }
  },
  created() {
    this.selectedEmployee = this.$store.getters['employees/employees'].find(
      (employee) => employee.id === this.id
    );
  },
};
</script>