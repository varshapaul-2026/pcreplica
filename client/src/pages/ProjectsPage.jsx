import { useState, useEffect } from 'react';
import {
  Box,
  Button,
  Paper,
  Typography,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Chip,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  TextField,
  MenuItem,
  Alert,
} from '@mui/material';
import AddIcon from '@mui/icons-material/Add';
import { projectService } from '../services/apiService';
import { useAuth } from '../contexts/AuthContext';

const projectStatuses = [
  { value: 1, label: 'Planning' },
  { value: 2, label: 'Active' },
  { value: 3, label: 'On Hold' },
  { value: 4, label: 'Completed' },
  { value: 5, label: 'Cancelled' },
];

const statusColors = {
  Planning: 'default',
  Active: 'success',
  'On Hold': 'warning',
  Completed: 'info',
  Cancelled: 'error',
};

export default function ProjectsPage() {
  const [projects, setProjects] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [openDialog, setOpenDialog] = useState(false);
  const [formData, setFormData] = useState({
    name: '',
    description: '',
    location: '',
    startDate: new Date().toISOString().split('T')[0],
    endDate: '',
    status: 2,
    budget: '',
    companyId: 1,
  });
  const { user } = useAuth();

  useEffect(() => {
    loadProjects();
  }, []);

  const loadProjects = async () => {
    try {
      setLoading(true);
      const data = await projectService.getAll();
      setProjects(data);
      setError('');
    } catch (err) {
      setError('Failed to load projects');
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  const handleOpenDialog = () => {
    setOpenDialog(true);
  };

  const handleCloseDialog = () => {
    setOpenDialog(false);
    setFormData({
      name: '',
      description: '',
      location: '',
      startDate: new Date().toISOString().split('T')[0],
      endDate: '',
      status: 2,
      budget: '',
      companyId: 1,
    });
  };

  const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value,
    });
  };

  const handleSubmit = async () => {
    try {
      const submitData = {
        ...formData,
        budget: formData.budget ? parseFloat(formData.budget) : null,
      };
      await projectService.create(submitData);
      handleCloseDialog();
      loadProjects();
    } catch (err) {
      setError('Failed to create project');
      console.error(err);
    }
  };

  const canCreateProject = user?.role === 'Admin' || user?.role === 'ProjectManager';

  return (
    <Box>
      <Box sx={{ display: 'flex', justifyContent: 'space-between', mb: 3 }}>
        <Typography variant="h4">Projects</Typography>
        {canCreateProject && (
          <Button
            variant="contained"
            startIcon={<AddIcon />}
            onClick={handleOpenDialog}
          >
            New Project
          </Button>
        )}
      </Box>

      {error && (
        <Alert severity="error" sx={{ mb: 2 }}>
          {error}
        </Alert>
      )}

      <TableContainer component={Paper}>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell>Name</TableCell>
              <TableCell>Location</TableCell>
              <TableCell>Start Date</TableCell>
              <TableCell>Status</TableCell>
              <TableCell align="right">Budget</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {loading ? (
              <TableRow>
                <TableCell colSpan={5} align="center">
                  Loading...
                </TableCell>
              </TableRow>
            ) : projects.length === 0 ? (
              <TableRow>
                <TableCell colSpan={5} align="center">
                  No projects found
                </TableCell>
              </TableRow>
            ) : (
              projects.map((project) => (
                <TableRow key={project.id}>
                  <TableCell>{project.name}</TableCell>
                  <TableCell>{project.location || '-'}</TableCell>
                  <TableCell>
                    {new Date(project.startDate).toLocaleDateString()}
                  </TableCell>
                  <TableCell>
                    <Chip
                      label={project.status}
                      color={statusColors[project.status]}
                      size="small"
                    />
                  </TableCell>
                  <TableCell align="right">
                    {project.budget
                      ? `$${project.budget.toLocaleString()}`
                      : '-'}
                  </TableCell>
                </TableRow>
              ))
            )}
          </TableBody>
        </Table>
      </TableContainer>

      <Dialog open={openDialog} onClose={handleCloseDialog} maxWidth="sm" fullWidth>
        <DialogTitle>Create New Project</DialogTitle>
        <DialogContent>
          <TextField
            autoFocus
            margin="dense"
            name="name"
            label="Project Name"
            type="text"
            fullWidth
            required
            value={formData.name}
            onChange={handleChange}
          />
          <TextField
            margin="dense"
            name="description"
            label="Description"
            type="text"
            fullWidth
            multiline
            rows={3}
            value={formData.description}
            onChange={handleChange}
          />
          <TextField
            margin="dense"
            name="location"
            label="Location"
            type="text"
            fullWidth
            value={formData.location}
            onChange={handleChange}
          />
          <TextField
            margin="dense"
            name="startDate"
            label="Start Date"
            type="date"
            fullWidth
            required
            value={formData.startDate}
            onChange={handleChange}
            InputLabelProps={{ shrink: true }}
          />
          <TextField
            margin="dense"
            name="endDate"
            label="End Date"
            type="date"
            fullWidth
            value={formData.endDate}
            onChange={handleChange}
            InputLabelProps={{ shrink: true }}
          />
          <TextField
            margin="dense"
            name="status"
            label="Status"
            select
            fullWidth
            required
            value={formData.status}
            onChange={handleChange}
          >
            {projectStatuses.map((option) => (
              <MenuItem key={option.value} value={option.value}>
                {option.label}
              </MenuItem>
            ))}
          </TextField>
          <TextField
            margin="dense"
            name="budget"
            label="Budget"
            type="number"
            fullWidth
            value={formData.budget}
            onChange={handleChange}
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={handleCloseDialog}>Cancel</Button>
          <Button onClick={handleSubmit} variant="contained">
            Create
          </Button>
        </DialogActions>
      </Dialog>
    </Box>
  );
}
