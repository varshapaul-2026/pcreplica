import { Box, Grid, Paper, Typography } from '@mui/material';
import { useAuth } from '../contexts/AuthContext';

export default function DashboardPage() {
  const { user } = useAuth();

  const stats = [
    { title: 'Active Projects', value: '12', color: '#1976d2' },
    { title: 'Open RFIs', value: '8', color: '#dc004e' },
    { title: 'Pending Tasks', value: '24', color: '#ff9800' },
    { title: 'Budget Status', value: '85%', color: '#4caf50' },
  ];

  return (
    <Box>
      <Typography variant="h4" gutterBottom>
        Welcome back, {user?.firstName}!
      </Typography>
      <Typography variant="body1" color="text.secondary" gutterBottom>
        Here's an overview of your construction projects
      </Typography>

      <Grid container spacing={3} sx={{ mt: 2 }}>
        {stats.map((stat) => (
          <Grid item xs={12} sm={6} md={3} key={stat.title}>
            <Paper
              elevation={3}
              sx={{
                p: 3,
                display: 'flex',
                flexDirection: 'column',
                alignItems: 'center',
                borderTop: `4px solid ${stat.color}`,
              }}
            >
              <Typography variant="h3" component="div" color={stat.color}>
                {stat.value}
              </Typography>
              <Typography variant="body1" color="text.secondary">
                {stat.title}
              </Typography>
            </Paper>
          </Grid>
        ))}
      </Grid>

      <Grid container spacing={3} sx={{ mt: 2 }}>
        <Grid item xs={12} md={6}>
          <Paper elevation={3} sx={{ p: 3 }}>
            <Typography variant="h6" gutterBottom>
              Recent Activity
            </Typography>
            <Typography variant="body2" color="text.secondary">
              No recent activity to display
            </Typography>
          </Paper>
        </Grid>
        <Grid item xs={12} md={6}>
          <Paper elevation={3} sx={{ p: 3 }}>
            <Typography variant="h6" gutterBottom>
              Upcoming Deadlines
            </Typography>
            <Typography variant="body2" color="text.secondary">
              No upcoming deadlines
            </Typography>
          </Paper>
        </Grid>
      </Grid>
    </Box>
  );
}
