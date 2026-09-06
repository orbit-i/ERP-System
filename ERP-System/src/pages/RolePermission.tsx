import React, { useState } from 'react';
import { ShieldCheck, Save, CheckSquare, Square } from 'lucide-react';

interface PermissionModule {
  id: string;
  name: string;
  canView: boolean;
  canCreate: boolean;
  canEdit: boolean;
  canDelete: boolean;
}

interface Role {
  id: string;
  name: string;
  description: string;
  permissions: PermissionModule[];
}

const RolePermission: React.FC = () => {
  const defaultModules: PermissionModule[] = [
    { id: 'users', name: 'User Management', canView: true, canCreate: true, canEdit: true, canDelete: false },
    { id: 'companies', name: 'Company Management', canView: true, canCreate: true, canEdit: true, canDelete: true },
    { id: 'branches', name: 'Branch Management', canView: true, canCreate: true, canEdit: true, canDelete: true },
    { id: 'audit', name: 'Audit Logs', canView: true, canCreate: false, canEdit: false, canDelete: false },
  ];

  const [roles, setRoles] = useState<Role[]>([
    { id: 'admin', name: 'Administrator', description: 'Full system control and access rights', permissions: defaultModules },
    { 
      id: 'manager', 
      name: 'Manager', 
      description: 'Can manage operations and companies', 
      permissions: defaultModules.map(m => ({ ...m, canDelete: false })) 
    },
    { 
      id: 'staff', 
      name: 'Staff', 
      description: 'Limited view and execution permissions', 
      permissions: defaultModules.map(m => ({ ...m, canCreate: false, canEdit: false, canDelete: false })) 
    },
  ]);

  const [selectedRoleId, setSelectedRoleId] = useState('admin');
  const [successMessage, setSuccessMessage] = useState(false);

  const currentRole = roles.find((r) => r.id === selectedRoleId) || roles[0];

  const handleTogglePermission = (moduleId: string, action: 'canView' | 'canCreate' | 'canEdit' | 'canDelete') => {
    setRoles(
      roles.map((role) => {
        if (role.id === selectedRoleId) {
          const updatedPermissions = role.permissions.map((perm) => {
            if (perm.id === moduleId) {
              return { ...perm, [action]: !perm[action] };
            }
            return perm;
          });
          return { ...role, permissions: updatedPermissions };
        }
        return role;
      })
    );
  };

  const handleSave = () => {
    setSuccessMessage(true);
    setTimeout(() => setSuccessMessage(false), 3000);
  };

  return (
    <div>
      <div className="flex flex-col sm:flex-row justify-between items-start sm:items-center gap-4 mb-6">
        <div>
          <h2 className="text-xl font-bold text-gray-800">Role & Permission Management</h2>
          <p className="text-sm text-gray-500">Configure access levels and module permissions for security roles.</p>
        </div>
        <button
          onClick={handleSave}
          className="flex items-center gap-2 bg-emerald-600 text-white px-4 py-2 rounded-lg text-sm font-semibold hover:bg-emerald-700 transition shadow-sm"
        >
          <Save size={18} /> Save Matrix
        </button>
      </div>

      {successMessage && (
        <div className="mb-6 p-4 bg-emerald-50 border border-emerald-200 text-emerald-700 text-sm rounded-lg flex items-center gap-2 animate-in fade-in">
          <ShieldCheck size={18} /> Permissions matrix updated successfully!
        </div>
      )}

      <div className="grid grid-cols-1 lg:grid-cols-4 gap-6">
        {/* Roles Selection Sidebar */}
        <div className="bg-white p-4 rounded-xl shadow-sm border border-gray-100 lg:col-span-1 space-y-2">
          <h3 className="text-xs font-semibold text-gray-400 uppercase tracking-wider px-3 mb-2">System Roles</h3>
          {roles.map((role) => (
            <button
              key={role.id}
              onClick={() => setSelectedRoleId(role.id)}
              className={`w-full text-left px-4 py-3 rounded-lg text-sm font-medium transition flex flex-col gap-1 ${
                selectedRoleId === role.id
                  ? 'bg-emerald-50 text-emerald-900 border border-emerald-200 shadow-sm'
                  : 'text-gray-600 hover:bg-gray-50'
              }`}
            >
              <div className="flex items-center justify-between">
                <span>{role.name}</span>
                <ShieldCheck size={16} className={selectedRoleId === role.id ? 'text-emerald-600' : 'text-gray-400'} />
              </div>
              <span className="text-xs text-gray-500 font-normal line-clamp-1">{role.description}</span>
            </button>
          ))}
        </div>

        {/* Permissions Matrix */}
        <div className="bg-white rounded-xl shadow-sm border border-gray-100 lg:col-span-3 overflow-hidden">
          <div className="p-4 border-b border-gray-100 bg-gray-50/50 flex justify-between items-center">
            <h3 className="font-semibold text-gray-800 text-sm">
              Permissions for <span className="text-emerald-600">{currentRole.name}</span>
            </h3>
            <span className="text-xs text-gray-500">Check or uncheck controls per module</span>
          </div>

          <table className="min-w-full divide-y divide-gray-200">
            <thead className="bg-gray-50">
              <tr>
                <th className="px-6 py-3.5 text-left text-xs font-semibold text-gray-500 uppercase tracking-wider">Module</th>
                <th className="px-6 py-3.5 text-center text-xs font-semibold text-gray-500 uppercase tracking-wider">View</th>
                <th className="px-6 py-3.5 text-center text-xs font-semibold text-gray-500 uppercase tracking-wider">Create</th>
                <th className="px-6 py-3.5 text-center text-xs font-semibold text-gray-500 uppercase tracking-wider">Edit</th>
                <th className="px-6 py-3.5 text-center text-xs font-semibold text-gray-500 uppercase tracking-wider">Delete</th>
              </tr>
            </thead>
            <tbody className="bg-white divide-y divide-gray-200">
              {currentRole.permissions.map((module) => (
                <tr key={module.id} className="hover:bg-gray-50/50 transition">
                  <td className="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900">{module.name}</td>
                  
                  <td className="px-6 py-4 whitespace-nowrap text-center">
                    <button
                      onClick={() => handleTogglePermission(module.id, 'canView')}
                      className="text-emerald-600 hover:text-emerald-800 transition"
                    >
                      {module.canView ? <CheckSquare size={20} className="mx-auto text-emerald-600" /> : <Square size={20} className="mx-auto text-gray-300" />}
                    </button>
                  </td>

                  <td className="px-6 py-4 whitespace-nowrap text-center">
                    <button
                      onClick={() => handleTogglePermission(module.id, 'canCreate')}
                      className="text-emerald-600 hover:text-emerald-800 transition"
                    >
                      {module.canCreate ? <CheckSquare size={20} className="mx-auto text-emerald-600" /> : <Square size={20} className="mx-auto text-gray-300" />}
                    </button>
                  </td>

                  <td className="px-6 py-4 whitespace-nowrap text-center">
                    <button
                      onClick={() => handleTogglePermission(module.id, 'canEdit')}
                      className="text-emerald-600 hover:text-emerald-800 transition"
                    >
                      {module.canEdit ? <CheckSquare size={20} className="mx-auto text-emerald-600" /> : <Square size={20} className="mx-auto text-gray-300" />}
                    </button>
                  </td>

                  <td className="px-6 py-4 whitespace-nowrap text-center">
                    <button
                      onClick={() => handleTogglePermission(module.id, 'canDelete')}
                      className="text-emerald-600 hover:text-emerald-800 transition"
                    >
                      {module.canDelete ? <CheckSquare size={20} className="mx-auto text-emerald-600" /> : <Square size={20} className="mx-auto text-gray-300" />}
                    </button>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </div>
    </div>
  );
};

export default RolePermission;