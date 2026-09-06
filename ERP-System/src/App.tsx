import React from 'react';
import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import DashboardLayout from './layouts/DashboardLayout';
import Login from './pages/Login';
import Register from './pages/Register';
import ForgotPassword from './pages/ForgotPassword';

// Placeholder components for the remaining pages
const UserManagement = () => <div className="p-6 text-2xl font-bold">User Management UI</div>;
const CompanyManagement = () => <div className="p-6 text-2xl font-bold">Company Management UI</div>;
const BranchManagement = () => <div className="p-6 text-2xl font-bold">Branch Management UI</div>;
const RolePermission = () => <div className="p-6 text-2xl font-bold">Role & Permission Management UI</div>;
const AuditLogs = () => <div className="p-6 text-2xl font-bold">Audit Logs UI</div>;

const App: React.FC = () => {
  return (
    <BrowserRouter>
      <Routes>
        {/* Auth Routes */}
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/forgot-password" element={<ForgotPassword />} />

        {/* Dashboard Layout Routes */}
        <Route path="/" element={<DashboardLayout />}>
          <Route path="users" element={<UserManagement />} />
          <Route path="companies" element={<CompanyManagement />} />
          <Route path="branches" element={<BranchManagement />} />
          <Route path="roles" element={<RolePermission />} />
          <Route path="audit-logs" element={<AuditLogs />} />
        </Route>

        {/* Fallback */}
        <Route path="*" element={<Navigate to="/login" replace />} />
      </Routes>
    </BrowserRouter>
  );
};

export default App;