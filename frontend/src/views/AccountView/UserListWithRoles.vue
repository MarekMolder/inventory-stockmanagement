<script setup lang="ts">
import { onMounted, ref } from "vue";
import { RoleService } from "@/services/RoleService";
import type { UserWithRolesDto } from "@/types/UserWithRolesDto";
import type { AppRole } from "@/domain/logic/AppRole";

const roleService = new RoleService();
const users = ref<UserWithRolesDto[]>([]);
const allRoles = ref<AppRole[]>([]);
const error = ref("");

const fetchUsers = async () => {
  try {
    users.value = await roleService.getAllUsersWithRoles();
  } catch (e: any) {
    error.value = e.message || "Viga kasutajate laadimisel";
  }
};

const fetchAllRoles = async () => {
  try {
    allRoles.value = await roleService.getAllRoles();
  } catch (e: any) {
    error.value = e.message || "Viga rollide laadimisel";
  }
};

const removeRole = async (userId: string, roleName: string) => {
  const role = allRoles.value.find(r => r.name === roleName);
  if (!role) {
    error.value = `Rolli '${roleName}' ei leitud`;
    return;
  }

  const result = await roleService.removeRoleFromUser(userId, role.id);
  if (!result.errors!.length) {
    await fetchUsers();
  } else {
    error.value = result.errors!.join(", ");
  }
};

onMounted(async () => {
  await fetchAllRoles();
  await fetchUsers();
});
</script>

<template>
  <main class="action-index-wrapper">
    <div class="action-index-box">
      <h1 class="title">{{ $t('Users and roles') }}</h1>

      <div v-if="error" style="color: red">{{ error }}</div>

      <table class="styled-table">
        <thead>
        <tr>
          <th>{{ $t('Email') }}</th>
          <th>{{ $t('Full name') }}</th>
          <th>{{ $t('Roles') }}</th>
        </tr>
        </thead>
        <tbody>
        <tr v-for="user in users" :key="user.id">
          <td>{{ user.email }}</td>
          <td>{{ user.firstName }} {{ user.lastName }}</td>
          <td>
              <span
                v-for="role in user.roles"
                :key="role"
                class="role-badge"
              >
                {{ role }}
                <button
                  class="remove-button"
                  @click="removeRole(user.id, role)"
                  title="Remove role"
                >
                  ×
                </button>
              </span>
          </td>
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
  max-width: 900px;
  box-shadow: 0 0 12px rgba(255, 165, 0, 0.4);
  border: 3px solid orange;
}

.title {
  text-align: center;
  font-size: 2rem;
  color: orange;
  margin-bottom: 1rem;
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

.role-badge {
  background-color: #444;
  color: white;
  border-radius: 5px;
  padding: 0.3rem 0.6rem;
  margin: 0.2rem;
  display: inline-flex;
  align-items: center;
}

.remove-button {
  background: none;
  border: none;
  color: red;
  margin-left: 0.5rem;
  cursor: pointer;
  font-weight: bold;
  font-size: 1rem;
  line-height: 1;
}
</style>
