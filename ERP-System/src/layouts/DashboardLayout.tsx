import React from 'react';
import { Outlet, Link, useNavigate, useLocation } from 'react-router-dom';
import { 
  LayoutDashboard, 
  Users, 
  Building2, 
  GitBranch, 
  ShieldCheck, 
  FileText, 
  LogOut, 
  Bell, 
  Search 
} from 'lucide-react';

const DashboardLayout: React.FC = () => {
  const navigate = useNavigate();
  const location = useLocation();

  const handleLogout = () => {
    localStorage.removeItem('token');
    navigate('/login');
  };

  const navItems = [
    { name: 'Dashboard', path: '/', icon: LayoutDashboard },
    { name: 'User Management', path: '/users', icon: Users },
    { name: 'Company Management', path: '/companies', icon: Building2 },
    { name: 'Branch Management', path: '/branches', icon: GitBranch },
    { name: 'Role & Permission', path: '/roles', icon: ShieldCheck },
    { name: 'Audit Logs', path: '/audit-logs', icon: FileText },
  ];

  return (
    <div className="flex h-screen bg-gray-100 overflow-hidden">
      {/* Sidebar */}
      <aside className="w-64 bg-emerald-900 text-white flex flex-col shadow-md">
        <div className="flex items-center h-16 px-6 text-xl font-bold tracking-wider border-b border-emerald-800">
          ORBIT-I ERP
        </div>
        
        <nav className="flex-1 px-4 py-6 space-y-1.5 overflow-y-auto">
          {navItems.map((item) => {
            const Icon = item.icon;
            const isActive = location.pathname === item.path;
            return (
              <Link
                key={item.name}
                to={item.path}
                className={`flex items-center gap-3 px-4 py-3 rounded-lg text-sm font-medium transition ${
                  isActive
                    ? 'bg-emerald-800 text-white shadow-sm'
                    : 'text-emerald-100 hover:bg-emerald-800/60'
                }`}
              >
                <Icon size={20} />
                {item.name}
              </Link>
            );
          })}
        </nav>

        <div className="p-4 border-t border-emerald-800">
          <button
            onClick={handleLogout}
            className="flex items-center gap-3 w-full px-4 py-2.5 text-red-200 hover:bg-emerald-800 rounded-lg text-sm font-medium transition"
          >
            <LogOut size={20} /> Logout
          </button>
        </div>
      </aside>

      {/* Main Content Area */}
      <div className="flex-1 flex flex-col min-w-0 overflow-hidden">
        {/* Topbar */}
        <header className="h-16 bg-white shadow-sm flex items-center justify-between px-6 z-10">
          <div className="flex items-center gap-4 w-96">
            <div className="relative w-full">
              <span className="absolute inset-y-0 left-0 flex items-center pl-3 text-gray-400">
                <Search size={16} />
              </span>
              <input
                type="text"
                placeholder="Search modules, records..."
                className="w-full rounded-lg border border-gray-200 bg-gray-50 py-2 pl-9 pr-4 text-sm focus:bg-white focus:border-emerald-500 focus:outline-none"
              />
            </div>
          </div>

          <div className="flex items-center gap-4">
            <button className="relative text-gray-500 hover:text-gray-700 p-2 rounded-full hover:bg-gray-100 transition">
              <Bell size={20} />
              <span className="absolute top-1.5 right-1.5 h-2 w-2 rounded-full bg-emerald-600"></span>
            </button>

            <div className="flex items-center gap-3 pl-4 border-l border-gray-200">
              <div className="text-right">
                <div className="text-sm font-semibold text-gray-800">Admin User</div>
                <div className="text-xs text-gray-500">System Admin</div>
              </div>
              <div className="h-10 w-10 rounded-full bg-emerald-600 text-white flex items-center justify-center font-bold text-sm shadow-sm">
                AU
              </div>
            </div>
          </div>
        </header>

        {/* View Outlet Container */}
        <main className="flex-1 overflow-x-hidden overflow-y-auto bg-gray-50 p-6">
          <Outlet />
        </main>
      </div>
    </div>
  );
};

export default DashboardLayout;