import React, { useState } from 'react';
import { Plus, Edit2, Trash2, Search, X, GitBranch, MapPin } from 'lucide-react';

interface Branch {
  id: number;
  name: string;
  company: string;
  city: string;
  phone: string;
  status: 'Active' | 'Inactive';
}

const BranchManagement: React.FC = () => {
  const [branches, setBranches] = useState<Branch[]>([
    { id: 1, name: 'Headquarters Branch', company: 'Orbit Technologies Ltd', city: 'New York', phone: '+1 (555) 111-2222', status: 'Active' },
    { id: 2, name: 'Regional Office', company: 'Orbit Technologies Ltd', city: 'San Francisco', phone: '+1 (555) 333-4444', status: 'Active' },
  ]);

  const [isModalOpen, setIsModalOpen] = useState(false);
  const [editingBranch, setEditingBranch] = useState<Branch | null>(null);
  const [searchTerm, setSearchTerm] = useState('');

  // Form states
  const [name, setName] = useState('');
  const [company, setCompany] = useState('Orbit Technologies Ltd');
  const [city, setCity] = useState('');
  const [phone, setPhone] = useState('');
  const [status, setStatus] = useState<'Active' | 'Inactive'>('Active');

  const handleOpenCreateModal = () => {
    setEditingBranch(null);
    setName('');
    setCompany('Orbit Technologies Ltd');
    setCity('');
    setPhone('');
    setStatus('Active');
    setIsModalOpen(true);
  };

  const handleOpenEditModal = (branch: Branch) => {
    setEditingBranch(branch);
    setName(branch.name);
    setCompany(branch.company);
    setCity(branch.city);
    setPhone(branch.phone);
    setStatus(branch.status);
    setIsModalOpen(true);
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (editingBranch) {
      setBranches(
        branches.map((b) => (b.id === editingBranch.id ? { ...b, name, company, city, phone, status } : b))
      );
    } else {
      const newBranch: Branch = {
        id: Date.now(),
        name,
        company,
        city,
        phone,
        status,
      };
      setBranches([...branches, newBranch]);
    }
    setIsModalOpen(false);
  };

  const handleDelete = (id: number) => {
    setBranches(branches.filter((b) => b.id !== id));
  };

  const filteredBranches = branches.filter(
    (b) =>
      b.name.toLowerCase().includes(searchTerm.toLowerCase()) ||
      b.city.toLowerCase().includes(searchTerm.toLowerCase()) ||
      b.company.toLowerCase().includes(searchTerm.toLowerCase())
  );

  return (
    <div>
      <div className="flex flex-col sm:flex-row justify-between items-start sm:items-center gap-4 mb-6">
        <div>
          <h2 className="text-xl font-bold text-gray-800">Branch Management</h2>
          <p className="text-sm text-gray-500">Manage corporate branch locations and company links.</p>
        </div>
        <button
          onClick={handleOpenCreateModal}
          className="flex items-center gap-2 bg-emerald-600 text-white px-4 py-2 rounded-lg text-sm font-semibold hover:bg-emerald-700 transition shadow-sm"
        >
          <Plus size={18} /> Add Branch
        </button>
      </div>

      <div className="bg-white p-4 rounded-xl shadow-sm border border-gray-100 mb-6 flex items-center justify-between">
        <div className="relative w-full max-w-sm">
          <span className="absolute inset-y-0 left-0 flex items-center pl-3 text-gray-400">
            <Search size={16} />
          </span>
          <input
            type="text"
            placeholder="Search by branch name, city, or company..."
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
            className="w-full rounded-lg border border-gray-200 py-2 pl-9 pr-4 text-sm focus:border-emerald-500 focus:outline-none"
          />
        </div>
      </div>

      <div className="bg-white shadow-sm rounded-xl border border-gray-100 overflow-hidden">
        <table className="min-w-full divide-y divide-gray-200">
          <thead className="bg-gray-50">
            <tr>
              <th className="px-6 py-3.5 text-left text-xs font-semibold text-gray-500 uppercase tracking-wider">Branch Name</th>
              <th className="px-6 py-3.5 text-left text-xs font-semibold text-gray-500 uppercase tracking-wider">Company</th>
              <th className="px-6 py-3.5 text-left text-xs font-semibold text-gray-500 uppercase tracking-wider">City / Location</th>
              <th className="px-6 py-3.5 text-left text-xs font-semibold text-gray-500 uppercase tracking-wider">Phone</th>
              <th className="px-6 py-3.5 text-left text-xs font-semibold text-gray-500 uppercase tracking-wider">Status</th>
              <th className="px-6 py-3.5 text-right text-xs font-semibold text-gray-500 uppercase tracking-wider">Actions</th>
            </tr>
          </thead>
          <tbody className="bg-white divide-y divide-gray-200">
            {filteredBranches.length > 0 ? (
              filteredBranches.map((branch) => (
                <tr key={branch.id} className="hover:bg-gray-50/50 transition">
                  <td className="px-6 py-4 whitespace-nowrap text-sm font-medium text-gray-900 flex items-center gap-2">
                    <GitBranch size={16} className="text-emerald-600" />
                    {branch.name}
                  </td>
                  <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-600">{branch.company}</td>
                  <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500 flex items-center gap-1.5 pt-5">
                    <MapPin size={14} className="text-gray-400" />
                    {branch.city}
                  </td>
                  <td className="px-6 py-4 whitespace-nowrap text-sm text-gray-500">{branch.phone}</td>
                  <td className="px-6 py-4 whitespace-nowrap">
                    <span
                      className={`px-2.5 py-1 text-xs font-medium rounded-full ${
                        branch.status === 'Active'
                          ? 'bg-emerald-50 text-emerald-700 border border-emerald-200'
                          : 'bg-gray-100 text-gray-600 border border-gray-200'
                      }`}
                    >
                      {branch.status}
                    </span>
                  </td>
                  <td className="px-6 py-4 whitespace-nowrap text-right text-sm font-medium">
                    <button
                      onClick={() => handleOpenEditModal(branch)}
                      className="text-emerald-600 hover:text-emerald-900 mr-3 p-1 rounded hover:bg-emerald-50 transition"
                      title="Edit Branch"
                    >
                      <Edit2 size={16} />
                    </button>
                    <button
                      onClick={() => handleDelete(branch.id)}
                      className="text-red-500 hover:text-red-700 p-1 rounded hover:bg-red-50 transition"
                      title="Delete Branch"
                    >
                      <Trash2 size={16} />
                    </button>
                  </td>
                </tr>
              ))
            ) : (
              <tr>
                <td colSpan={6} className="px-6 py-8 text-center text-sm text-gray-500">
                  No branches found matching your search.
                </td>
              </tr>
            )}
          </tbody>
        </table>
      </div>

      {isModalOpen && (
        <div className="fixed inset-0 bg-black/50 flex items-center justify-center z-50 p-4">
          <div className="bg-white rounded-xl shadow-xl w-full max-w-md overflow-hidden animate-in fade-in zoom-in duration-150">
            <div className="flex justify-between items-center px-6 py-4 border-b border-gray-100">
              <h3 className="text-lg font-bold text-gray-900">
                {editingBranch ? 'Edit Branch Record' : 'Add New Branch'}
              </h3>
              <button
                onClick={() => setIsModalOpen(false)}
                className="text-gray-400 hover:text-gray-600 p-1 rounded-lg"
              >
                <X size={20} />
              </button>
            </div>

            <form onSubmit={handleSubmit} className="p-6 space-y-4">
              <div>
                <label className="block text-sm font-medium text-gray-700 mb-1">Branch Name</label>
                <input
                  type="text"
                  value={name}
                  onChange={(e) => setName(e.target.value)}
                  required
                  placeholder="Downtown Hub"
                  className="w-full rounded-lg border border-gray-300 py-2 px-3 text-sm focus:border-emerald-500 focus:outline-none"
                />
              </div>

              <div>
                <label className="block text-sm font-medium text-gray-700 mb-1">Associated Company</label>
                <select
                  value={company}
                  onChange={(e) => setCompany(e.target.value)}
                  className="w-full rounded-lg border border-gray-300 py-2 px-3 text-sm focus:border-emerald-500 focus:outline-none bg-white"
                >
                  <option value="Orbit Technologies Ltd">Orbit Technologies Ltd</option>
                  <option value="Global Logistics Corp">Global Logistics Corp</option>
                </select>
              </div>

              <div>
                <label className="block text-sm font-medium text-gray-700 mb-1">City / Location</label>
                <input
                  type="text"
                  value={city}
                  onChange={(e) => setCity(e.target.value)}
                  required
                  placeholder="New York"
                  className="w-full rounded-lg border border-gray-300 py-2 px-3 text-sm focus:border-emerald-500 focus:outline-none"
                />
              </div>

              <div>
                <label className="block text-sm font-medium text-gray-700 mb-1">Phone Number</label>
                <input
                  type="text"
                  value={phone}
                  onChange={(e) => setPhone(e.target.value)}
                  required
                  placeholder="+1 (555) 000-0000"
                  className="w-full rounded-lg border border-gray-300 py-2 px-3 text-sm focus:border-emerald-500 focus:outline-none"
                />
              </div>

              <div>
                <label className="block text-sm font-medium text-gray-700 mb-1">Status</label>
                <select
                  value={status}
                  onChange={(e) => setStatus(e.target.value as 'Active' | 'Inactive')}
                  className="w-full rounded-lg border border-gray-300 py-2 px-3 text-sm focus:border-emerald-500 focus:outline-none bg-white"
                >
                  <option value="Active">Active</option>
                  <option value="Inactive">Inactive</option>
                </select>
              </div>

              <div className="flex justify-end gap-3 pt-4 border-t border-gray-100">
                <button
                  type="button"
                  onClick={() => setIsModalOpen(false)}
                  className="px-4 py-2 rounded-lg border border-gray-300 text-sm font-medium text-gray-700 hover:bg-gray-50 transition"
                >
                  Cancel
                </button>
                <button
                  type="submit"
                  className="px-4 py-2 rounded-lg bg-emerald-600 text-white text-sm font-semibold hover:bg-emerald-700 transition shadow-sm"
                >
                  {editingBranch ? 'Save Changes' : 'Create Branch'}
                </button>
              </div>
            </form>
          </div>
        </div>
      )}
    </div>
  );
};

export default BranchManagement;