<script setup lang="ts">
import { onMounted, ref } from "vue";
import { RoleService } from "@/services/RoleService";
import type { AppRole } from "@/domain/logic/AppRole";

const roleService = new RoleService();
const roles = ref<AppRole[]>([]);
const error = ref("");
const success = ref("");
const newRoleName = ref("");

const fetchRoles = async () => {
  try {
    roles.value = await roleService.getAllRoles();
  } catch (e: any) {
    error.value = e.message;
  }
};

const createRole = async () => {
  error.value = "";
  success.value = "";

  if (!newRoleName.value.trim()) {
    error.value = "Rolli nimi ei tohi olla tühi";
    return;
  }

  const result = await roleService.createRole(newRoleName.value.trim());
  if (result.errors!.length) {
    error.value = result.errors!.join(", ");
  } else {
    success.value = result.data!;
    newRoleName.value = "";
    await fetchRoles();
  }
};

onMounted(fetchRoles);
</script>

<template>
  <main class="action-index-wrapper">
    <div class="action-index-box">
      <h1 class="title">{{ $t('All roles') }}</h1>

      <div v-if="error" class="error" style="color: red">{{ error }}</div>
      <div v-if="success" class="success" style="color: limegreen">{{ success }}</div>

      <form @submit.prevent="createRole" class="create-role-form">
        <input v-model="newRoleName" type="text" placeholder="Enter new role name" class="role-input" />
        <button type="submit" class="create-button">{{ $t('Create role') }}</button>
      </form>

      <table class="styled-table">
        <thead>
        <tr>
          <th>ID</th>
          <th>Name</th>
        </tr>
        </thead>
        <tbody>
        <tr v-for="role in roles" :key="role.id">
          <td>{{ role.id }}</td>
          <td>{{ role.name }}</td>
        </tr>
        </tbody>
      </table>
    </div>
  </main>
</template>

<style scoped>
.action-index-wrapper {
  display: flex;
  justify-content: center;
  margin-top: 5vh;
  font-family: Arial, sans-serif;
  color: white;
}

.action-index-box {
  background-color: #1a1a1a;
  padding: 2rem;
  border-radius: 12px;
  width: 90%;
  max-width: 800px;
  box-shadow: 0 0 12px rgba(255, 165, 0, 0.4);
  border: 3px solid orange;
}

.title {
  text-align: center;
  font-size: 2rem;
  color: orange;
  margin-bottom: 1rem;
}

.create-role-form {
  display: flex;
  gap: 1rem;
  margin-bottom: 1.5rem;
}

.role-input {
  flex: 1;
  padding: 0.5rem;
  border: 2px solid orange;
  border-radius: 6px;
  background-color: #2a2a2a;
  color: white;
}

.create-button {
  background-color: orange;
  padding: 0.5rem 1rem;
  border: none;
  border-radius: 6px;
  font-weight: bold;
  cursor: pointer;
}

.create-button:hover {
  background-color: #ffaa33;
}

.styled-table {
  width: 100%;
  border-collapse: collapse;
  background-color: #2a2a2a;
}

.styled-table th,
.styled-table td {
  border: 1px solid #444;
  padding: 0.75rem;
  text-align: left;
}

.styled-table thead {
  background-color: #ff8c00;
  color: white;
}
</style>
