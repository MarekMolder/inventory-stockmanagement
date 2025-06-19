<script setup lang="ts">
import { onMounted, ref } from "vue";
import { RoleService } from "@/services/RoleService";
import type { AppRole } from "@/domain/logic/AppRole";
import { useUserDataStore } from "@/stores/userDataStore"; // ← lisa see

const roleService = new RoleService();
const roles = ref<AppRole[]>([]);
const error = ref("");
const store = useUserDataStore();

const fetchRoles = async () => {
  console.log("JWT in store:", store.jwt);
  try {
    roles.value = await roleService.getAllRoles();
  } catch (e: any) {
    error.value = e.message;
  }
};

onMounted(fetchRoles);
</script>

<template>
  <main class="action-index-wrapper">
    <div class="action-index-box">
      <h1 class="title">{{ $t('All roles') }}</h1>
      <div v-if="error" class="error">{{ error }}</div>
      <table class="styled-table">
        <thead>
        <tr>
          <th>ID</th>
          <th>{{ $t('Name') }}</th>
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

.status-filter {
  background-color: #2a2a2a;
  color: white;
  border: 2px solid orange;
  padding: 0.4rem 0.8rem;
  border-radius: 8px;
  font-size: 1rem;
  font-weight: 500;
  margin-bottom: 1rem;
  width: 100%;
}

.create-link {
  background-color: orange;
  padding: 0.5rem 1rem;
  color: white;
  text-decoration: none;
  border-radius: 6px;
  font-weight: bold;
  transition: background 0.3s ease;
  display: block;
  width: 100%;
  text-align: center;
}

.create-link:hover {
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
