import React, { useState } from 'react';
import { Search, Shield, User, Globe, Clock } from 'lucide-react';

interface AuditLog {
  id: number;
  user: string;
  email: string;
  action: string;
  module: string;
  ipAddress: string;
  timestamp: string;
}

const AuditLogs: React.FC = () => {
  const [logs] = useState<AuditLog[]>([
    { 
      id: 1, 
      user: 'John Doe', 
      email: 'john@orbit.com', 
      action: 'CREATED_USER', 
      module: 'User Management', 
      ipAddress: '192.168.1.45', 
      timestamp: '2026-09-01 14:32:01' 
    },
    { 
      id: 2, 
      user: 'Jane Smith', 
      email: 'jane@orbit.com', 
      action: 'UPDATE_COMPANY', 
      module: 'Company Management', 
      ipAddress: '192.168.1.12', 
      timestamp: '2026-09-01 12:15:44' 
    },
    { 
      id: 3, 
      user: 'Admin User', 
      email: 'admin@orbit.com', 
      action: 'DELETE_BRANCH', 
      module: 'Branch Management', 
      ipAddress: '192.168.1.1', 
      timestamp: '2026-08-31 09:45:10' 
    },
    { 
      id: 4, 
      user: 'John Doe', 
      email: 'john@orbit.com', 
      action: 'UPDATE_ROLES', 
      module: 'Role & Permission', 
      ipAddress: '192.168.1.45', 
      timestamp: '2026-08-30 16:20:05' 
    },
  ]);

  const [searchTerm, setSearchTerm] = useState('');
  const [moduleFilter, setModuleFilter] = useState('ALL');

  const filteredLogs = logs.filter((log) => {
    const matchesSearch = 
      log.user.toLowerCase().includes(searchTerm.toLowerCase()) ||
      log.action.toLowerCase().includes(searchTerm.toLowerCase()) ||
      log.ipAddress.includes(searchTerm);
    
    const matchesModule = moduleFilter === 'ALL' || log.module === moduleFilter;

    return matchesSearch && matchesModule;
  });

  return (
    <div>
      <div className="flex flex-col sm:flex-row justify-between items-start sm:items-center gap-4 mb-6">
        <div>
          <h2 className="text-xl font-bold text-gray-800">System Audit Logs</h2>
          <p className="text-sm text-gray-500">Track user actions, changes, and security events across modules.</p>
        </div>
      </div>

      {/* Filter and Search Bar */}
      <div className="bg-white p-4 rounded-xl shadow-sm border border-gray-100 mb-6 flex flex-col sm:flex-row items-center justify-between gap-4">
        <div className="relative w-full max-w-sm">
          <span className="absolute inset-y-0 left-0 flex items-center pl-3 text-gray-400">
            <Search size={16} />
          </span>
          <input
            type="text"
            placeholder="Search by user, action, or IP..."
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
            className="w-full rounded-lg border border-gray-200 py-2 pl-9 pr-4 text-sm focus:border-emerald-500 focus:outline-none"
          />
        </div>

        <div className="w-full sm:w-auto flex items-center gap-2">
          <span className="text-sm text-gray-500 whitespace-nowrap">Filter Module:</span>
          <select
            value={moduleFilter}
            onChange={(e) => setModuleFilter(e.target.value)}
            className="rounded-lg border border-gray-200 py-2 px-3 text-sm focus:border-emerald-500 focus:outline-none bg-white w-full sm:w-auto"
          >
            <option value="ALL">All Modules</option>
            <option value="User Management">User Management</option>
            <option value="Company Management">Company Management</option>
            <option value="Branch Management">Branch Management</option>
            <option value="Role & Permission">Role & Permission</option>
          </select>
        </div>
      </div>

      {/* Audit Logs Table */}
      <div className="bg-white shadow-sm rounded-xl border border-gray-100 overflow-hidden">
        <table className="min-w-full divide-y divide-gray-200">
          <thead className="bg-gray-50">
            <tr>
              <th className="px-6 py-3.5 text-left text-xs font-semibold text-gray-500 uppercase tracking-wider">User</th>
              <th className="px-6 py-3.5 text-left text-xs font-semibold text-gray-500 uppercase tracking-wider">Action Performed</th>
              <th className="px-6 py-3.5 text-left text-xs font-semibold text-gray-500 uppercase tracking-wider">Module</th>
              <th className="px-6 py-3.5 text-left text-xs font-semibold text-gray-500 uppercase tracking-wider">IP Address</th>
              <th className="px-6 py-3.5 text-left text-xs font-semibold text-gray-500 uppercase tracking-wider">Timestamp</th>
            </tr>
          </thead>
          <tbody className="bg-white divide-y divide-gray-200">
            {filteredLogs.length > 0 ? (
              filteredLogs.map((log) => (
                <tr key={log.id} className="hover:bg-gray-50/50 transition">
                  <td className="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900 flex items-center gap-2">
                    <User size={16} className="text-emerald-600" />
                    <div>
                      <div>{log.user}</div>
                      <div className="text-xs text-gray-400 font-normal">{log.email}</div>
                    </div>
                  </td>
                  <td className="px-6 py-4 whitespace-nowrap">
                    <span className="px-2.5 py-1 text-xs font-mono font-semibold text-emerald-700 bg-emerald-50 rounded border border-emerald-200">
                      {log.action}
                    </span>
                  </td>
                  <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-600 flex items-center gap-1.5 pt-5">
                    <Shield size={14} className="text-gray-400" />
                    {log.module}
                  </td>
                  <td className="px-6 py-4 whitespace-nowrap text-sm font-mono text-gray-500 flex items-center gap-1.5 pt-5">
                    <Globe size={14} className="text-gray-400" />
                    {log.ipAddress}
                  </td>
                  <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500 flex items-center gap-1.5 pt-5">
                    <Clock size={14} className="text-gray-400" />
                    {log.timestamp}
                  </td>
                </tr>
              ))
            ) : (
              <tr>
                <td colSpan={5} className="px-6 py-8 text-center text-sm text-gray-500">
                  No audit logs found matching your filters.
                </td>
              </tr>
            )}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default AuditLogs;